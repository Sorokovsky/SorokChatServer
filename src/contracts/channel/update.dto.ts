import { ChannelKey } from "./key.type";
import { OmitType, PartialType } from '@nestjs/swagger';
import { Channel } from "src/database/entities/channel.entity";

export const updateChannelKeys: ChannelKey[] = ['title', 'description'];

export class UpdateChannelDto extends PartialType(OmitType(Channel, updateChannelKeys)) { };