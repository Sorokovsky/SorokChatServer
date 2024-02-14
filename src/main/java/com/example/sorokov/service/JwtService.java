package com.example.sorokov.service;

import com.example.sorokov.exception.JwtException;
import com.example.sorokov.model.TokensModel;
import com.example.sorokov.model.UserModel;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.common.collect.Lists;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import net.oauth.jsontoken.Checker;
import net.oauth.jsontoken.JsonToken;
import net.oauth.jsontoken.JsonTokenParser;
import net.oauth.jsontoken.crypto.HmacSHA256Signer;
import net.oauth.jsontoken.crypto.HmacSHA256Verifier;
import net.oauth.jsontoken.crypto.SignatureAlgorithm;
import net.oauth.jsontoken.crypto.Verifier;
import net.oauth.jsontoken.discovery.VerifierProvider;
import net.oauth.jsontoken.discovery.VerifierProviders;
import org.joda.time.Instant;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import java.io.IOException;
import java.security.InvalidKeyException;
import java.security.SignatureException;
import java.util.List;

@Service
public class JwtService {
    private final String SECRET_KEY;
    private final String ISSUER = "SorokChat";
    private final String AUDIENCE = "NotReallyImportant";

    public JwtService(@Value("${secret.key}") String secret_key) {
        SECRET_KEY = secret_key;
    }

    public TokensModel generateTokens(UserModel user) {
        try {
            long EXPIRATION_ACCESS_TIME = 1000 * 60 * 30;
            long EXPIRATION_REFRESH_TIME = 1000 * 60 * 60 * 24 * 7;
            String accessToken = generateToken(user, EXPIRATION_ACCESS_TIME);
            String refreshToken = generateToken(user, EXPIRATION_REFRESH_TIME);
            return new TokensModel(accessToken, refreshToken);
        } catch (JwtException jwtException) {
            throw new JwtException();
        }
    }

    private String generateToken(UserModel user, long expirationTime) {
        long currentTimeInMillis = System.currentTimeMillis();
        HmacSHA256Signer signer;
        try {
            signer = new HmacSHA256Signer(ISSUER, null, SECRET_KEY.getBytes());

        } catch (InvalidKeyException exception) {
            throw new JwtException(exception.getMessage());
        }
        JsonToken token = new JsonToken(signer);
        token.setAudience(AUDIENCE);
        token.setIssuedAt(new Instant(currentTimeInMillis));
        token.setExpiration(new Instant(currentTimeInMillis + expirationTime));
        JsonObject request = new JsonObject();
        ObjectMapper mapper = new ObjectMapper();
        try {
            request.addProperty("user", mapper.writeValueAsString(user));
        } catch (JsonProcessingException exception) {
            throw new JwtException(exception.getMessage());
        }
        JsonObject payload = token.getPayloadAsJsonObject();
        payload.add("info", request);
        try {
            return token.serializeAndSign();

        }
        catch (SignatureException exception) {
            throw new JwtException(exception.getMessage());
        }
    }

    public UserModel extractUser(String token) {
        try {
            final Verifier hmacVerifier = new HmacSHA256Verifier(SECRET_KEY.getBytes());
            VerifierProvider hmacLocator = new VerifierProvider() {
                @Override
                public List<Verifier> findVerifier(String id, String key) {
                    return Lists.newArrayList(hmacVerifier);
                }
            };
            VerifierProviders locators = new VerifierProviders();
            locators.setVerifierProvider(SignatureAlgorithm.HS256, hmacLocator);
            Checker checker = new Checker() {
                @Override
                public void check(JsonObject payload) throws SignatureException {

                }
            };
            JsonTokenParser parser = new JsonTokenParser(locators, checker);
            JsonToken jt;
            try {
                jt = parser.verifyAndDeserialize(token);
            } catch (SignatureException e) {
                throw new JwtException(e.getMessage());
            }
            JsonObject payload = jt.getPayloadAsJsonObject();
            JsonElement user = payload.getAsJsonObject("info").get("user");
            ObjectMapper mapper = new ObjectMapper();
            UserModel userModel = mapper.readValue(user.getAsString(), UserModel.class);
            return userModel;
        }  catch (InvalidKeyException | IOException e) {
            throw new JwtException(e.getMessage());
        }
    }

}
