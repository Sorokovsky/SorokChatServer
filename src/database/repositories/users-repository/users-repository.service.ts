import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { UsersRepository } from 'src/abstractions/users-repository.interface';
import { CreateUserDto } from 'src/contracts/user/create.dto';
import { UpdateUserDto } from 'src/contracts/user/update.dto';
import { User } from "../../entities/user.entity";
import { UserAlreadyExistsException } from '../../../exceptions/user/already-exists.exception';
import { Repository } from 'typeorm';
import { UserNotFoundException } from '../../../exceptions/user/not-found.exception';

@Injectable()
export class UsersRepositoryService implements UsersRepository {
    constructor(
        @InjectRepository(User) private readonly usersRepository: Repository<User>,
    ) {};

    async findAll(): Promise<User[]> {
        return await this.usersRepository.find();
    }

    async tryFindById(id: User['id']): Promise<User | null> {
        return await this.usersRepository.findOneBy({id});
    }

    async tryFindByEmail(email: string): Promise<User | null> {
        return await this.usersRepository.findOneBy({email});
    }

    async create(newUser: CreateUserDto): Promise<User> {
        const candidate = await this.tryFindByEmail(newUser.email);
        if(candidate) throw new UserAlreadyExistsException('email', candidate.email);
        const created = this.usersRepository.create(newUser);
        await this.usersRepository.save(created);
        return created;
    }

    async update(id: User['id'], newUser: UpdateUserDto): Promise<User> {
        const candidate = await this.tryFindById(id);
        if(candidate === null) throw new UserNotFoundException('id', id);
        const updated = await this.usersRepository.update({id}, newUser);
        return await this.tryFindById(id);
    }

    async delete(id: User['id']): Promise<User> {
        const candidate = await this.tryFindById(id);
        if(candidate === null) throw new UserNotFoundException('id', id);
        await this.usersRepository.delete({id});
        return candidate;
    }
};
