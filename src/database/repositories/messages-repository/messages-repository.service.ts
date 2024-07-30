import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { MessagesRepository } from 'src/abstractions/messages-repository.interface';
import { CreateMessageDto } from 'src/contracts/message/create.dto';
import { UpdateMessageDto } from 'src/contracts/message/update.dto';
import { Channel } from 'src/database/entities/channel.entity';
import { Message } from 'src/database/entities/message.entity';
import { User } from 'src/database/entities/user.entity';
import { Repository } from 'typeorm';
import { MessageNotFoundException } from '../../../exceptions/message/not-found.exception';

@Injectable()
export class MessagesRepositoryService implements MessagesRepository {
    constructor(
        @InjectRepository(Message) private messagesRepository: Repository<Message>
    ) { }

    async tryFindById(id: Message['id']): Promise<Message | null> {
        return await this.messagesRepository.findOneBy({ id });
    }

    async findByUserId(userId: User['id']): Promise<Message[]> {
        return await this.messagesRepository.findBy({ author: { id: userId } });
    }

    async findByChannelId(channelId: Channel['id']): Promise<Message[]> {
        return await this.messagesRepository.findBy({ channel: { id: channelId } });
    }

    async create(newMessage: CreateMessageDto): Promise<Message> {
        const created = this.messagesRepository.create(newMessage);
        await this.messagesRepository.save(created);
        return created;
    }

    async update(id: Message['id'], newMessage: UpdateMessageDto): Promise<Message> {
        const candidate = await this.tryFindById(id);
        if (candidate === null) throw new MessageNotFoundException('id', id);
        await this.messagesRepository.update({ id }, newMessage);
        return await this.tryFindById(id);
    }

    async delete(id: Message['id']): Promise<Message> {
        const candidate = this.tryFindById(id);
        if (candidate === null) throw new MessageNotFoundException('id', id);
        await this.messagesRepository.delete({ id });
        return candidate;
    }
;
};
