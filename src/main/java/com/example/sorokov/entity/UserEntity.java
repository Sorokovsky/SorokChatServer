package com.example.sorokov.entity;
import jakarta.persistence.*;
import lombok.*;
import lombok.experimental.Accessors;
import org.hibernate.annotations.SourceType;
import org.hibernate.annotations.UpdateTimestamp;
import org.springframework.data.annotation.CreatedDate;

import java.util.Date;

@Entity
@Builder
@Table(name = "\"user\"")
@Data
@AllArgsConstructor
@NoArgsConstructor
@Accessors(chain = true)
public class UserEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String surname;
    private String name;
    @CreatedDate
    @Column(name = "created_at", nullable = false)
    private Date createdAt;
    @Column(unique = true, nullable = false)
    private String email;
    @Column(name = "updated_at", nullable = false)
    @UpdateTimestamp(source = SourceType.DB)
    private Date updatedAt;
    @Column(name = "avatar_path")
    private String avatarPath;
    private String password;

}
