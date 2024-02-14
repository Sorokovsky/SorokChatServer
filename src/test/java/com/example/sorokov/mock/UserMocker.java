package com.example.sorokov.mock;

import com.example.sorokov.entity.UserEntity;
import com.example.sorokov.model.UserModel;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class UserMocker {
    public static List<UserModel> getUserModelList(int length) {
        if(length < 1) {
            throw new RuntimeException("Length of list could not to be a smaller than 0");
        }
        List<UserModel> list = new ArrayList<>();
        for (long i = 0L; i < length; i++) {
            list.add(UserMocker.getUserModel(i + 1));
        }
        return list;
    }
    public static List<UserEntity> getUserEntityList(int length) {
        if(length < 1) {
            throw new RuntimeException("Length of list could not to be a smaller than 0");
        }
        List<UserEntity> list = new ArrayList<>();
        for (long i = 0L; i < length; i++) {
            list.add(UserMocker.getUserEntity(i + 1));
        }
        return list;
    }
    public static UserEntity getUserEntity(Long id) {
        return new UserEntity()
                .setId(id)
                .setEmail("Sorokovsky" + id + "@ukr.net")
                .setName("Andrey")
                .setAvatarPath("")
                .setSurname("Sorokovsky")
                .setCreatedAt(new Date())
                .setUpdatedAt(new Date())
                .setPassword("Password");
    }
    public static UserModel getUserModel(Long id) {
        return new UserModel(
                id,
                "Sorokovsky",
                "Andrey",
                new Date(),
                new Date(),
                "",
                "Sorokovsky" + id + "@ukr.net"

        );
    }

}
