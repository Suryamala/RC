import { TestBed } from '@angular/core/testing';

import { ApiBuilderService } from './api-builder.service';

describe('ApiBuilderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApiBuilderService = TestBed.get(ApiBuilderService);
    expect(service).toBeTruthy();
  });
});
