import { ConfigService } from '@nestjs/config';
import type { TypeOrmModuleOptions } from '@nestjs/typeorm';
import { Channel } from '../database/entities/channel.entity';
import { Message } from '../database/entities/message.entity';
import { User } from '../database/entities/user.entity';
export const getTypeOrmConfiguration = (configService: ConfigService): TypeOrmModuleOptions => {
    return {
        type: 'postgres',
        host: configService.get("DB_HOST"),
        port: configService.get<number>("DB_PORT"),
        username: configService.get("DB_USER"),
        password: configService.get("DB_PASSWORD"),
        database: configService.get("DB_NAME"),
        entities: [User, Message, Channel],
        synchronize: true,
    };
};