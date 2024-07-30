import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Message } from 'src/database/entities/message.entity';
import { MessagesRepositoryService } from './messages-repository.service';

@Module({
  imports: [TypeOrmModule.forFeature([Message])],
  providers: [MessagesRepositoryService]
})
export class MessagesRepositoryModule { };
