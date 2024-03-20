import TComponentFilters from 'types/components/TComponentFilters.ts';

type TComponent<T extends File | string> = TComponentFilters & {
  id: number;
  country: string;
  price: number;
  description: string;
  image: T;
  amount: number;
};

export default TComponent;
