package com.example.sorokov.service;

import com.example.sorokov.entity.UserEntity;
import com.example.sorokov.exception.NotCreatedException;
import com.example.sorokov.exception.NotDeletedException;
import com.example.sorokov.exception.NotFoundException;
import com.example.sorokov.exception.NotUpdatedException;
import com.example.sorokov.mapper.UserMapper;
import com.example.sorokov.model.UserModel;
import com.example.sorokov.repository.UserRepository;
import org.springframework.dao.DataAccessException;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class UserService {
    private final UserRepository repository;

    public UserService(UserRepository repository) {
        this.repository = repository;
    }

    public UserModel getById(Long id) {
        return UserMapper.toModel(repository.findById(id).orElseThrow(
                () -> new NotFoundException("User where id + " + id + " not founded")
        ));
    }
    
    public List<UserModel> getAll() {
        return repository.findAll().stream().map(UserMapper::toModel).collect(Collectors.toList());
    }

    public UserModel create(UserEntity userEntity) {
        try {
            return UserMapper.toModel(repository.save(userEntity));
        } catch (DataAccessException exception) {
            throw new NotCreatedException("User not created");
        }
    }

    public UserModel delete(Long id) {
        try {
            UserModel user = getById(id);
            repository.deleteById(id);
            return user;
        } catch (DataAccessException exception) {
            throw new NotDeletedException("user not deleted");
        }
    }

    public UserModel updateById(Long id, UserEntity entity) {
        try {
            UserEntity toUpdateEntity = entity.setId(id);
            return UserMapper.toModel(repository.save(toUpdateEntity));
        } catch (DataAccessException exception) {
            throw new NotUpdatedException("user not updated");
        }
    }
}
