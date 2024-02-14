package com.example.sorokov.mapper;

import com.example.sorokov.entity.UserEntity;
import com.example.sorokov.model.UserModel;

public class UserMapper {
    static public UserModel toModel(UserEntity userEntity) {
        return new UserModel(
                userEntity.getId(),
                userEntity.getSurname(),
                userEntity.getName(),
                userEntity.getCreatedAt(),
                userEntity.getUpdatedAt(),
                userEntity.getAvatarPath(),
                userEntity.getEmail()
        );
    }
    static public UserEntity toEntity(UserModel userModel) {
        return UserEntity.builder()
                .id(userModel.getId())
                .name(userModel.getName())
                .surname(userModel.getSurname())
                .avatarPath(userModel.getAvatarPath())
                .updatedAt(userModel.getUpdatedAt())
                .createdAt(userModel.getCreatedAt())
                .email(userModel.getEmail())
                .build();
    }
}
