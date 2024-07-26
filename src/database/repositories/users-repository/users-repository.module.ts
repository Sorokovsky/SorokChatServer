import { Module } from '@nestjs/common';
import { UsersRepositoryService } from './users-repository.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { User } from 'src/database/entities/user.entity';
import { UsersRepository } from 'src/abstractions/users-repository.abstracts';

@Module({
  imports: [TypeOrmModule.forFeature([User])],
  providers: [
    {
      provide: UsersRepository,
      useClass: UsersRepositoryService
    },
  ],
  exports: [UsersRepository]
})
export class UsersRepositoryModule {};
