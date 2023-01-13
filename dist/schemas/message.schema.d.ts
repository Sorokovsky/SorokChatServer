import { HydratedDocument } from 'mongoose';
import * as mongoose from 'mongoose';
import { User } from './user.schema';
import { Channel } from './channel.schema';
export type MessageDocument = HydratedDocument<Message>;
export declare class Message {
    author: User;
    channel: Channel;
    body: string;
    time: Date;
}
export declare const MessageSchema: mongoose.Schema<Message, mongoose.Model<Message, any, any, any, any>, {}, {}, {}, {}, mongoose.DefaultSchemaOptions, Message>;
