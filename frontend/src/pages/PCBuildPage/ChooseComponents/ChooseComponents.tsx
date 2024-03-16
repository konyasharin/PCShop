import ChooseComponent from './ChooseComponent/ChooseComponent.tsx';
import videoCardIcon from 'assets/videocard-white-icon.png';
import processorIcon from 'assets/cpu-white-icon.png';
import styles from './ChooseComponents.module.css';
import EComponentTypes from 'enums/EComponentTypes.ts';
import { useEffect, useRef } from 'react';
import TProduct from 'types/TProduct.ts';
import TVideoCard from 'types/components/TVideoCard.ts';
import useComponents from 'hooks/useComponents.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import TComputerCase from 'types/components/TComputerCase.ts';
import TProcessor from 'types/components/TProcessor.ts';

function convertToTProduct<
  T extends TVideoCard<string> | TComputerCase<string> | TProcessor<string>,
>(components: T[]): TProduct[] {
  return components.map(component => {
    return {
      img: component.image,
      name: `${component.brand} ${component.model}`,
      description: component.description,
      price: component.price,
    };
  });
}

function ChooseComponents() {
  // const [videoCards, setVideoCards] = useState<TProduct[]>([]);
  // useEffect(() => {
  //   const newState: TProduct[] = [];
  //   getComponents<TVideoCard<string>[]>(
  //     '/VideoCard/getAllVideoCards',
  //     3,
  //     0,
  //   ).then(response => {
  //     response.data.data.forEach(videoCard => {
  //       newState.push({
  //         name: videoCard.model,
  //         price: videoCard.price,
  //         img: videoCard.image,
  //         description: videoCard.description,
  //       });
  //     });
  //   });
  //   setVideoCards(newState);
  // }, []);
  const { components: videoCards, getComponents: getVideoCards } =
    useComponents<TVideoCard<string>>('/VideoCard/getAllVideoCards');
  const { components: computerCases, getComponents: getComputerCases } =
    useComponents<TComputerCase<string>>('/ComputerCase/getAllComputerCases');
  const { components: processors, getComponents: getProcessors } =
    useComponents<TProcessor<string>>('/Processor/getAllProcessors');
  const dispatch = useDispatch();
  const isLoaded = useRef(false);

  useEffect(() => {
    if (isLoaded.current) return;
    isLoaded.current = true;
    dispatch(setIsLoading(true));
    getVideoCards();
    getProcessors();
    getComputerCases();
    dispatch(setIsLoading(false));
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
        products={convertToTProduct(videoCards)}
        onShowMore={() => getVideoCards()}
      />
      <ChooseComponent
        img={processorIcon}
        type={EComponentTypes.processor}
        title={'Процессор'}
        isImportant={true}
        errorType={'Warning'}
        searchTitle={'Выберите процессор'}
        products={convertToTProduct(processors)}
        onShowMore={() => getProcessors()}
      />
    </section>
  );
}

export default ChooseComponents;
