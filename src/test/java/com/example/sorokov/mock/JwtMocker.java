package com.example.sorokov.mock;

import com.example.sorokov.model.TokensModel;
import com.example.sorokov.model.UserModel;

public class JwtMocker {
    static public TokensModel getExpectedTokens() {
        return new TokensModel("access", "refresh");
    }

    static public String getExpectedToken() {
        return "expected_token";
    }

    static public String getNotExpectedToken() {
        return "not_expected_token";
    }

    static public UserModel getExpectedUser() {
        return UserMocker.getUserModel(1L);
    }

    static public UserModel getNotExpectedUser() {
        return UserMocker.getUserModel(2L);
    }

}
