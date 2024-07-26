import { Channel } from "src/database/entities/channel.entity";
import { Message } from "src/database/entities/message.entity";
import { User } from "src/database/entities/user.entity";
import { CreateChannelDto } from '../contracts/channel/create.dto';

export interface ChannelRepositotry {
    findById(id: Channel['id']): Promise<Channel>;

    findByUserId(userId: User['id']): Promise<Channel[]>;

    findByMessageId(messageId: Message['id']): Promise<Channel>;

    create(newChannel: CreateChannelDto): Promise<Channel>;
};