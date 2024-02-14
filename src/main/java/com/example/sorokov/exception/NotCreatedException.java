package com.example.sorokov.exception;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(value = HttpStatus.BAD_REQUEST)
public class NotCreatedException extends RuntimeException {
    public NotCreatedException(String message) {
        super(message);
    }
}
