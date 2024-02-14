package com.example.sorokov.model;

public class TokensModel {
    private final String accessToken;
    private final String refreshToken;

    public TokensModel(String accessToken, String refreshToken) {
        this.accessToken = accessToken;
        this.refreshToken = refreshToken;
    }

    public String getAccessToken() {
        return accessToken;
    }

    public String getRefreshToken() {
        return refreshToken;
    }

}
