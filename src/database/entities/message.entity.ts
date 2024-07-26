import { ApiProperty } from "@nestjs/swagger";
import { IsString } from "class-validator";
import { Column, Entity, ManyToOne, JoinColumn, JoinTable } from 'typeorm';
import { Base } from "./base.entity";
import { Channel } from "./channel.entity";
import { User } from "./user.entity";

@Entity('messages')
export class Message extends Base {
    @ApiProperty({default: "text"})
    @IsString()
    @Column()
    text: string;

    @ManyToOne(() => User, user => user.messages)
    @JoinColumn({name: 'author_id'})
    @JoinTable()
    author: User;

    @ManyToOne(() => Channel, channel => channel.messages)
    @JoinColumn({name: 'channel_id', referencedColumnName: "id",})
    channel: Channel;
};