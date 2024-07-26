import { Base } from "./base.entity";
import { ApiProperty } from '@nestjs/swagger';
import { IsOptional, IsString } from "class-validator";
import { Column, Entity, JoinColumn, JoinTable, ManyToMany, OneToMany } from 'typeorm';
import { Message } from "./message.entity";
import { User } from "./user.entity";

@Entity("channels")
export class Channel extends Base {
    @ApiProperty({default: "My channel"})
    @IsString()
    @Column({nullable: true})
    title: string;

    @ApiProperty({default: "My best channel ever"})
    @IsString()
    @Column()
    @IsOptional()
    description?: string;

    @OneToMany(() => Message, message => message.channel, {cascade: true})
    @JoinColumn({name: "message_id"})
    @JoinTable()
    messages: Message[];

    @ManyToMany(() => User, user => user.channels)
    @JoinTable({
        joinColumn: {
            name: "member_id",
            referencedColumnName: "id",
        },
        inverseJoinColumn: {
            name: 'channel_id',
            referencedColumnName: "id",
        },
    })
    members: User[];
};