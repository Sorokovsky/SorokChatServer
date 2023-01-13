import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { HydratedDocument } from 'mongoose';
import * as mongoose from 'mongoose';
import { User } from './user.schema';
import { Message } from './message.schema';
export type ChannelDocument = HydratedDocument<Channel>;
@Schema()
export class Channel {
  @Prop({required:true, index:true, unique:true})
  name: string;
  @Prop({type: [{type:mongoose.Schema.Types.ObjectId, ref: 'User'}]})
  members: User[];
  @Prop({type: [{type:mongoose.Schema.Types.ObjectId, ref: 'User'}]})
  admins: User[];
  @Prop({type: [{type:mongoose.Schema.Types.ObjectId, ref: 'Message'}]})
  messages: Message[];
  @Prop({required:true})
  description: string;
  @Prop()
  avatar: string;
  @Prop()
  background: string;
}

export const ChannelSchema = SchemaFactory.createForClass(Channel)