import { Module } from '@nestjs/common';
import { UsersRepositoryService } from './users-repository.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { User } from 'src/database/entities/user.entity';

@Module({
  imports: [TypeOrmModule.forFeature([User])],
  providers: [UsersRepositoryService]
})
export class UsersRepositoryModule {};
