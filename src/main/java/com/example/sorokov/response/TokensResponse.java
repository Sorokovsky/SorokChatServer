package com.example.sorokov.response;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(value = HttpStatus.CREATED, code = HttpStatus.CREATED)
@RequiredArgsConstructor
@AllArgsConstructor
@Schema(description = "tokens schema")
public class TokensResponse {
    @Schema(description = "access token", example = "token")
    private String token;
}
