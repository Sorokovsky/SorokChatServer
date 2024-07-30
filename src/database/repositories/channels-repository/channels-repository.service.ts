import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { CreateChannelDto } from 'src/contracts/channel/create.dto';
import { UpdateChannelDto } from 'src/contracts/channel/update.dto';
import { Message } from 'src/database/entities/message.entity';
import { User } from 'src/database/entities/user.entity';
import { Repository } from 'typeorm';
import { ChannelsRepositotry } from '../../../abstractions/channels-repository.interface';
import { Channel } from '../../entities/channel.entity';

@Injectable()
export class ChannelsRepositoryService implements ChannelsRepositotry {
    constructor(
        @InjectRepository(Channel) private channelsRepository: Repository<Channel>,
    ) { };
    
    async tryFindById(id: Channel['id']): Promise<Channel | null> {
        return await this.channelsRepository.findOneBy({ id })
    }

    async findByUserId(userId: User['id']): Promise<Channel[]> {
        return await this.channelsRepository.find({ where: { members: { id: userId } } });
    }

    async tryFindByMessageId(messageId: Message['id']): Promise<Channel | null> {
        return await this.channelsRepository.findOne({ where: { messages: { id: messageId } } });
    }

    async create(newChannel: CreateChannelDto): Promise<Channel> {
        throw new Error('Method not implemented.');
    }

    async update(id: Channel['id'], newChannel: UpdateChannelDto): Promise<Channel> {
        throw new Error('Method not implemented.');
    }

    async delete(id: Channel['id']): Promise<Channel> {
        throw new Error('Method not implemented.');
    }
    
};
