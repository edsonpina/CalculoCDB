import { TestBed } from '@angular/core/testing';

import { CalculocdbService } from './calculocdb.service';

describe('CalculocdbService', () => {
  let service: CalculocdbService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CalculocdbService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
