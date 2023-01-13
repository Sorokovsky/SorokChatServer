import { HydratedDocument } from 'mongoose';
import * as mongoose from 'mongoose';
import { User } from './user.schema';
import { Message } from './message.schema';
export type ChannelDocument = HydratedDocument<Channel>;
export declare class Channel {
    name: string;
    members: User[];
    admins: User[];
    messages: Message[];
    description: string;
    avatar: string;
    background: string;
}
export declare const ChannelSchema: mongoose.Schema<Channel, mongoose.Model<Channel, any, any, any, any>, {}, {}, {}, {}, mongoose.DefaultSchemaOptions, Channel>;
