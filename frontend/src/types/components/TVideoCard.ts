import TComponent from 'types/components/TComponent.ts';

type TVideoCard<T extends File | string> = TComponent<T> & {
  memory_db: string;
  memory_type: string;
};

export default TVideoCard;
