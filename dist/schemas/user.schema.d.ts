import { HydratedDocument } from 'mongoose';
import * as mongoose from 'mongoose';
import { Channel } from './channel.schema';
export type UserDocument = HydratedDocument<User>;
export declare class User {
    email: string;
    surname: string;
    name: string;
    nickname: string;
    password: string;
    avatar: string;
    contacts: User[];
    channels: Channel[];
    role: string;
}
export declare const UserSchema: mongoose.Schema<User, mongoose.Model<User, any, any, any, any>, {}, {}, {}, {}, mongoose.DefaultSchemaOptions, User>;
