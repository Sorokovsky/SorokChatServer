import { UpdateChannelDto } from "src/contracts/channel/update.dto";
import { Channel } from "src/database/entities/channel.entity";
import { Message } from "src/database/entities/message.entity";
import { User } from "src/database/entities/user.entity";
import { CreateChannelDto } from '../contracts/channel/create.dto';

export interface ChannelsRepositotry {
    tryFindById(id: Channel['id']): Promise<Channel | null>;

    findByUserId(userId: User['id']): Promise<Channel[]>;

    tryFindByMessageId(messageId: Message['id']): Promise<Channel | null>;

    create(newChannel: CreateChannelDto): Promise<Channel>;

    update(id: Channel['id'], newChannel: UpdateChannelDto): Promise<Channel>;

    delete(id: Channel['id']): Promise<Channel>;
};