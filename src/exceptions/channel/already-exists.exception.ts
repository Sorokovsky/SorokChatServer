import { Channel } from 'src/database/entities/channel.entity';
import { AlreadyExistsException } from '../base/already-exists.exception';
export class ChannelAlreadyExistsException extends AlreadyExistsException<Channel> {
    constructor(key: keyof Channel, value: any) {
        super("Чат", key, value);
    };
};