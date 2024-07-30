import { UpdateMessageDto } from "src/contracts/message/update.dto";
import { Channel } from "src/database/entities/channel.entity";
import { Message } from "src/database/entities/message.entity";
import { User } from "src/database/entities/user.entity";
import { CreateMessageDto } from '../contracts/message/create.dto';

export interface MessagesRepository {
    tryFindById(id: Message['id']): Promise<Message | null>;

    findByUserId(userId: User['id']): Promise<Message[]>;

    findByChannelId(channelId: Channel['id']): Promise<Message[]>;

    create(newMessage: CreateMessageDto): Promise<Message>;

    update(id: Message['id'], newMessage: UpdateMessageDto): Promise<Message>;

    delete(id: Message['id']): Promise<Message>;
};