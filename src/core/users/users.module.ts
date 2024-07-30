import { Global, Module } from '@nestjs/common';
import { UsersService } from './users.service';
import { UsersController } from './users.controller';
import { UsersRepositoryModule } from '../../database/repositories/users-repository/users-repository.module';
import { UsersRepositoryService } from '../../database/repositories/users-repository/users-repository.service';

@Global()
@Module({
  imports: [UsersRepositoryModule],
  controllers: [UsersController],
  providers: [UsersService],
  exports: [UsersService]
})
export class UsersModule { };
