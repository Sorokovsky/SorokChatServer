import { PickType } from "@nestjs/swagger";
import { Channel } from "src/database/entities/channel.entity";
import { ChannelKey } from "./key.type";

export const createChannelKeys: ChannelKey[] = ['title', 'description'];

export class CreateChannelDto extends PickType(Channel, createChannelKeys) { };