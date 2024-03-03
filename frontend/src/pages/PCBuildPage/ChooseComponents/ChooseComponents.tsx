import ChooseComponent from './ChooseComponent/ChooseComponent.tsx';
import videoCardIcon from 'assets/videocard-white-icon.png';
import processorIcon from 'assets/cpu-white-icon.png';
import styles from './ChooseComponents.module.css';
import EComponentTypes from 'enums/EComponentTypes.ts';
import { useEffect, useState } from 'react';
import TProduct from 'types/TProduct.ts';
import getComponents from 'api/components/getComponents.ts';
import TVideoCard from 'types/components/TVideoCard.ts';

function ChooseComponents() {
  const [videoCards, setVideoCards] = useState<TProduct[]>([]);
  useEffect(() => {
    const newState: TProduct[] = [];
    getComponents<TVideoCard<string>[]>('/VideoCard/getAllVideoCards').then(
      response => {
        response.data.data.forEach(videoCard => {
          newState.push({
            name: videoCard.model,
            price: videoCard.price,
            img: videoCard.image,
            description: videoCard.description,
          });
        });
      },
    );
    setVideoCards(newState);
  }, []);
  return (
    <section className={styles.blocks}>
      <ChooseComponent
        img={videoCardIcon}
        type={EComponentTypes.videoCard}
        title={'Видеокарта'}
        isImportant={true}
        errorType={'Success'}
        searchTitle={'Выберите видеокарту'}
        products={videoCards}
      />
      <ChooseComponent
        img={processorIcon}
        type={EComponentTypes.processor}
        title={'Процессор'}
        isImportant={true}
        errorType={'Warning'}
        searchTitle={'Выберите процессор'}
        products={[]}
      />
    </section>
  );
}

export default ChooseComponents;
