import TComponent from 'types/components/TComponent.ts';

type TVideoCard<T extends File | string> = TComponent<T> & {
  memoryDb: string;
  memoryType: string;
};

export default TVideoCard;
