import { Module } from '@nestjs/common';
import { UsersRepositoryService } from './users-repository.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { User } from '../../entities/user.entity';

@Module({
  imports: [TypeOrmModule.forFeature([User])],
  providers: [UsersRepositoryService],
  exports: [UsersRepositoryService]
})
export class UsersRepositoryModule {};
