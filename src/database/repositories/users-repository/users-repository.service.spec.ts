import { Test, TestingModule } from '@nestjs/testing';
import { UsersRepositoryService } from './users-repository.service';
import { UsersRepository } from '../../../abstractions/users-repository.interface';
import { UsersRepositoryModule } from './users-repository.module';

describe('UsersRepositoryService', () => {
  let service: UsersRepository;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      imports: [UsersRepositoryModule]
    }).compile();

    service = module.get<UsersRepository>(UsersRepositoryService);
  });

  it('List must be ampty', async () => {
    const result = await service.findAll();
    expect(result).toEqual([]);
  });
});
