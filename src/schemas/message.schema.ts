import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { HydratedDocument } from 'mongoose';
import * as mongoose from 'mongoose';
import { User } from './user.schema';
import { Channel } from './channel.schema';
export type MessageDocument = HydratedDocument<Message>;
@Schema()
export class Message {
    @Prop({required:true, type: {type:mongoose.Schema.Types.ObjectId, ref: 'User'}})  
    author: User;
    @Prop({required:true, type: {type:mongoose.Schema.Types.ObjectId, ref: 'Channel'}})
    channel: Channel;
    @Prop({required:true})
    body:string;
    @Prop({default: new Date()})
    time:Date;
}

export const MessageSchema = SchemaFactory.createForClass(Message)