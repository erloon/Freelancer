import { TestBed } from '@angular/core/testing';

import { ValidationRulesService } from './validation-rules.service';

describe('ValidationRulesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ValidationRulesService = TestBed.get(ValidationRulesService);
    expect(service).toBeTruthy();
  });
});
