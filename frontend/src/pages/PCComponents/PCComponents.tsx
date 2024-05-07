import styles from './PCComponents.module.css';
import Container from 'components/Container/Container.tsx';
import useRadios from 'hooks/useRadios.ts';
import initCategories from '../AdminPage/Edit/initCategories.ts';
import { useEffect, useState } from 'react';
import componentTypes from 'enums/componentTypes.ts';
import CategoriesBlocks from '../AdminPage/Edit/CategoriesBlocks/CategoriesBlocks.tsx';
import getComponents from 'api/components/getComponents.ts';
import TOneOfComponents from 'types/components/TOneOfComponents.ts';
import ChooseComponentCard from 'components/cards/ChooseComponentCard/ChooseComponentCard.tsx';
import config from '../../../config.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';

function PCComponents() {
  const { radios: categories, setRadioIsActive: setCategoryIsActive } =
    useRadios(initCategories());
  const [activeCategory, setActiveCategory] =
    useState<keyof typeof componentTypes>('videoCard');
  const [components, setComponents] = useState<TOneOfComponents<string>[]>([]);
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(setIsLoading(true));
    getComponents(activeCategory, 3, 0).then(response => {
      setComponents(response.data.data);
      dispatch(setIsLoading(false));
    });
  }, [activeCategory]);
  const componentsBlocks = components.map(component => {
    return (
      <ChooseComponentCard
        url={`/product/${activeCategory}/${component.productId}`}
        text={component.description}
        price={component.price}
        name={`${component.brand} ${component.model}`}
        img={`${config.backupUrl}/${component.image}`}
      />
    );
  });
  return (
    <Container className={styles.block}>
      <h2 className={styles.text}>Компоненты</h2>
      <CategoriesBlocks
        categories={categories}
        setCategoryIsActive={setCategoryIsActive}
        setActiveCategory={setActiveCategory}
        className={styles.categories}
      />
      {...componentsBlocks}
    </Container>
  );
}

export default PCComponents;
