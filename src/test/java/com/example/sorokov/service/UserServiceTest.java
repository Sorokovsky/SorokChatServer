package com.example.sorokov.service;

import com.example.sorokov.entity.UserEntity;
import com.example.sorokov.exception.NotCreatedException;
import com.example.sorokov.exception.NotDeletedException;
import com.example.sorokov.exception.NotFoundException;
import com.example.sorokov.exception.NotUpdatedException;
import com.example.sorokov.mapper.UserMapper;
import com.example.sorokov.mock.UserMocker;
import com.example.sorokov.model.UserModel;
import com.example.sorokov.repository.UserRepository;
import org.junit.jupiter.api.*;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.jupiter.MockitoExtension;

import java.util.List;
import java.util.Optional;

@ExtendWith(MockitoExtension.class)
public class UserServiceTest {
    private List<UserEntity> expectedEntities;
    private List<UserModel> expectedModels;
    private Long expectedId;
    private Long notExpectedId;

    @Mock
    private UserRepository userRepository;
    @InjectMocks
    private UserService userService;

    @DisplayName("get all test success")
    @Test
    public void getAllSuccess() {

        Mockito.when(userRepository.findAll()).thenReturn(expectedEntities);
        List<UserModel> users = userService.getAll();
        Assertions.assertIterableEquals(expectedModels, users);
    }

    @DisplayName("get one test success")
    @Test
    public void getOneSuccess() {
        Optional<UserEntity> expected = Optional.of(expectedEntities.get(0));
        UserModel expectedModel = expectedModels.get(0);
        Mockito.when(userRepository.findById(expectedId)).thenReturn(expected);
        UserModel user = userService.getById(expectedId);
        Assertions.assertEquals(expectedModel, user);
    }

    @DisplayName("create test success")
    @Test
    public void createUserSuccess() {
        UserEntity entity = expectedEntities.get(0);
        UserModel model = expectedModels.get(0);
        Mockito.when(userRepository.save(entity)).thenReturn(entity);
        UserModel userModel = userService.create(entity);
        Assertions.assertEquals(model, userModel);
    }

    @DisplayName("delete user success")
    @Test
    public void deleteUserSuccess() {
        Optional<UserEntity> expectedEntity = Optional.ofNullable(expectedEntities.get(0));
        UserModel expectedModel = expectedModels.get(0);
        UserRepository repository = Mockito.spy(UserRepository.class);
        UserService service = new UserService(repository);
        Mockito.doNothing().when(repository).deleteById(expectedId);
        Mockito.doReturn(expectedEntity).when(repository).findById(expectedId);
        Assertions.assertEquals(expectedModel, service.delete(expectedId));
    }

    @DisplayName("update user success")
    @Test
    public void updateUserSuccess() {
        UserEntity toUpdateEntity = new UserEntity().setName("Anna");
        UserEntity updatedEntity = expectedEntities.get(0).setName("Anna").setId(expectedId);
        UserModel updatedModel = UserMapper.toModel(updatedEntity);
        UserRepository repository = Mockito.spy(UserRepository.class);
        Mockito.doReturn(updatedEntity).when(repository).save(toUpdateEntity);
        UserService service = new UserService(repository);
        Assertions.assertEquals(updatedModel, service.updateById(expectedId, toUpdateEntity));
    }

    @DisplayName("get one wrong")
    @Test
    public void getOneUserWrong() {
        NotFoundException notFoundException = new NotFoundException("User not founded");
        Mockito.when(userRepository.findById(notExpectedId)).thenThrow(notFoundException);
        Assertions.assertThrows(NotFoundException.class, () -> userService.getById(notExpectedId));
    }

    @DisplayName("create test wrong")
    @Test
    public void createUserWrong() {
        NotCreatedException notCreatedException = new NotCreatedException("User not created");
        Mockito.when(userRepository.save(expectedEntities.get(0))).thenThrow(notCreatedException);
        Assertions.assertThrows(NotCreatedException.class, () -> userService.create(expectedEntities.get(0)));
    }

    @DisplayName("delete test wrong")
    @Test
    public void deleteUserWrong() {
        NotDeletedException notDeletedException = new NotDeletedException("User not deleted");
        UserRepository repository = Mockito.spy(UserRepository.class);
        UserService service = new UserService(repository);
        Mockito.doThrow(notDeletedException).when(repository).findById(notExpectedId);
        Assertions.assertThrows(NotDeletedException.class, () -> service.delete(notExpectedId));
    }

    @DisplayName("update user wrong")
    @Test
    public void updateUserWrong() {
        UserEntity toUpdateEntity = new UserEntity().setName("Anna");
        UserRepository repository = Mockito.spy(UserRepository.class);
        Mockito.doThrow(NotUpdatedException.class).when(repository).save(toUpdateEntity.setId(notExpectedId));
        UserService service = new UserService(repository);
        Assertions.assertThrows(NotUpdatedException.class, () -> service.updateById(notExpectedId, toUpdateEntity));
    }

    @BeforeEach
    public void setup() {
        expectedEntities = UserMocker.getUserEntityList(1);
        expectedModels = UserMocker.getUserModelList(1);
        expectedId = 1L;
        notExpectedId = 2L;
    }

}