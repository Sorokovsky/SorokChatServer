package com.example.sorokov.repository;

import com.example.sorokov.entity.UserEntity;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepository extends JpaRepository<UserEntity, Long> {
    void deleteById(Long id);
}
