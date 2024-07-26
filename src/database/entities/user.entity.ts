import { Column, Entity, OneToMany, JoinColumn, ManyToMany, JoinTable } from 'typeorm';
import { Base } from "./base.entity";
import { ApiProperty } from '@nestjs/swagger';
import { IsEmail, IsOptional, IsString, MinLength } from 'class-validator';
import { Message } from "./message.entity";
import { Channel } from './channel.entity';

@Entity("users")
export class User extends Base {
    @ApiProperty({default: "Sorokovskys@ukr.net"})
    @IsEmail()
    @Column({unique: true, nullable: false})
    email: string;

    @ApiProperty({default: "postgres"})
    @IsString()
    @MinLength(7)
    @Column()
    password: string;

    @ApiProperty({default: "Sorokovsky"})
    @IsString()
    @IsOptional()
    @Column()
    surname?: string;

    @ApiProperty({default: "Andrey"})
    @IsString()
    @IsOptional()
    @Column()
    name?: string;

    @ApiProperty({default: "Ivanovich"})
    @IsString()
    @IsOptional()
    @Column({name: "middle_name"})
    middleName?: string;

    @OneToMany(() => Message, message => message.author)
    @JoinColumn({name: "message_id"})
    @JoinTable()
    messages: Message[];

    @ManyToMany(() => Channel, channel => channel.members)
    @JoinColumn({name: "channel_id", referencedColumnName: 'id'})
    channels: Channel[];
};