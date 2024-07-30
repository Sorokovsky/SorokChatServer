import { NotFoundException } from "../base/not-found.exception";
import { Channel } from 'src/database/entities/channel.entity';

export class ChannelNotFoundException extends NotFoundException<Channel> { 
    constructor(key: keyof Channel, value: any) {
        super('Чату', key, value);
    };
};