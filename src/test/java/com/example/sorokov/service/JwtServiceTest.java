package com.example.sorokov.service;

import com.example.sorokov.exception.JwtException;
import com.example.sorokov.mock.JwtMocker;
import com.example.sorokov.model.TokensModel;
import com.example.sorokov.model.UserModel;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.beans.factory.annotation.Autowired;

@ExtendWith(MockitoExtension.class)
public class JwtServiceTest {
    private TokensModel expectedTokens;
    private String expectedToken;
    private String notExpectedToken;
    private UserModel expectedUser;
    private UserModel notExpectedUser;

    private JwtService service = new JwtService("secret");


    @DisplayName("generate tokens success")
    @Test
    public void generateTokensSuccess() {
        Mockito.when(service.generateTokens(expectedUser)).thenReturn(expectedTokens);
        Assertions.assertEquals(expectedTokens, service.generateTokens(expectedUser));
    }

    @DisplayName("generate tokens wrong")
    @Test
    public void generateTokensWrong() {
        JwtException exception = new JwtException();
        Mockito.when(service.generateTokens(notExpectedUser)).thenThrow(exception);
        Assertions.assertThrows(JwtException.class, () -> service.generateTokens(notExpectedUser));
    }


    @BeforeEach
    public void setup() {
        expectedTokens = JwtMocker.getExpectedTokens();
        expectedToken = JwtMocker.getExpectedToken();
        notExpectedUser = JwtMocker.getNotExpectedUser();
        notExpectedToken = JwtMocker.getNotExpectedToken();
        expectedUser = JwtMocker.getExpectedUser();
        service = Mockito.spy(service);
    }

}
