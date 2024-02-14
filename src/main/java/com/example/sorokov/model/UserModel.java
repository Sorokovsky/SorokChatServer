package com.example.sorokov.model;
import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.*;
import lombok.experimental.Accessors;

import java.util.Date;

@Data
@Accessors(chain = true)
@Getter
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class UserModel {
    @JsonProperty("id")
    private Long id;
    @JsonProperty("surname")
    private String surname;
    @JsonProperty("name")
    private String name;
    @JsonProperty("created_at")
    private Date createdAt;
    @JsonProperty("updated_at")
    private Date updatedAt;
    @JsonProperty("avatar_path")
    private String avatarPath;
    @JsonProperty("email")
    private String email;
}
