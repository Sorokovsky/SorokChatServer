import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { HydratedDocument } from 'mongoose';
import * as mongoose from 'mongoose';
import { Role } from 'src/types/role';
import { Channel } from './channel.schema';
export type UserDocument = HydratedDocument<User>;
@Schema()
export class User {
  @Prop({required: true, index:true, unique:true})
  email: string;
  @Prop({required: true}) 
  surname: string;
  @Prop({required: true}) 
  name: string;
  @Prop({default: function() {return `${this.surname} ${this.name}`} }) 
  nickname: string;
  @Prop({required:true})
  password: string;
  @Prop({required:false})
  avatar: string;
  @Prop({type: [{type:mongoose.Schema.Types.ObjectId, ref: 'User'}]})
  contacts:User[];
  @Prop({type: [{type:mongoose.Schema.Types.ObjectId, ref: 'Channel'}]})
  channels:Channel[];
  @Prop({default: Role.USER})
  role:string;
}
export const UserSchema = SchemaFactory.createForClass(User)