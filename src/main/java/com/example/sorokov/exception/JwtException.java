package com.example.sorokov.exception;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(value = HttpStatus.BAD_REQUEST)
public class JwtException extends RuntimeException {

    public JwtException() {
        super("Jwt exception");
    }

    public JwtException(String message) {
        super(message);
    }

}
