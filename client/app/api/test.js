import { GET } from './util/methods';

export const test = async () => {
   await GET('test');
};

export const error = async () => {
   await GET('test/error');
};
