import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { ChannelsRepositoryService } from './channels-repository.service';
import { Channel } from '../../entities/channel.entity';

@Module({
  imports: [TypeOrmModule.forFeature([Channel])],
  providers: [ChannelsRepositoryService]
})
export class ChannelsRepositoryModule {}
