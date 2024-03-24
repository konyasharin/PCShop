import CategoriesBlocks from '../CategoriesBlocks/CategoriesBlocks.tsx';
import useRadios from 'hooks/useRadios.ts';
import React, { useEffect, useRef, useState } from 'react';
import EComponentTypes from 'enums/EComponentTypes.ts';
import styles from './AddProduct.module.css';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';
import emptyImg from 'assets/empty-img.png';
import Btn from 'components/btns/Btn/Btn.tsx';
import createProduct from 'api/components/createProduct.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import useFilters from 'hooks/useFilters.ts';
import TCheckBox from 'types/TCheckBox.ts';
import { TFilter } from 'store/slices/filtersSlice.ts';
import TVideoCard from 'types/components/TVideoCard.ts';
import useBorderValues from 'hooks/useBorderValues.ts';

function searchActiveFromFilter(filter: TFilter): TCheckBox | null {
  let result: null | TCheckBox = null;
  filter.filters.forEach(filterElem => {
    if (filterElem.isActive) {
      result = filterElem;
    }
  });
  return result;
}

function handleCreateProduct<T>(url: string, object: Omit<T, 'id'>) {
  return createProduct<Omit<T, 'id'>>(url, object);
}

function AddProduct() {
  const { radios: categories, setRadioIsActive: setCategoryIsActive } =
    useRadios([
      { text: 'Видеокарты', isActive: true },
      { text: 'Процессоры', isActive: false },
    ]);
  const [activeCategory, setActiveCategory] = useState<EComponentTypes>(
    EComponentTypes.videoCard,
  );
  const [productName, setProductName] = useState('');
  const [price, setPrice] = useState(0);
  const [img, setImg] = useState(emptyImg);
  const [description, setDescription] = useState('');
  const [amount, setAmount] = useState(0);
  const [power, setPower] = useBorderValues(0, 0, 10);
  const imgFile: React.MutableRefObject<null | File> = useRef(null);
  const allFiltersState = useSelector((state: RootState) => state.filters);
  const { filters, setRadioIsActive, setComponentTypeHandle } = useFilters(
    allFiltersState,
    'radio',
    activeCategory,
  );
  useEffect(() => {
    setComponentTypeHandle(activeCategory);
  }, [activeCategory]);

  return (
    <div className={styles.block}>
      <CategoriesBlocks
        categories={categories}
        setCategoryIsActive={setCategoryIsActive}
        setActiveCategory={setActiveCategory}
        className={styles.checkBoxes}
      />
      <EditProductInfo
        type={activeCategory}
        img={img}
        productName={productName}
        setProductName={setProductName}
        price={price}
        setPrice={setPrice}
        setImg={setImg}
        description={description}
        setDescription={setDescription}
        amount={amount}
        setAmount={setAmount}
        power={power}
        setPower={setPower}
        filters={filters}
        setRadioIsActive={setRadioIsActive}
        imgFileRef={imgFile}
      />
      <Btn
        onClick={() => {
          const filtersObject = Object.keys(filters).reduce(
            (acc, key) => {
              const active = searchActiveFromFilter(
                filters[key as keyof typeof filters],
              );
              acc[key as keyof typeof filters] = active ? active.text : '';
              return acc;
            },
            {} as { [key in keyof typeof filters]: string },
          );
          const requestData = {
            ...filtersObject,
            country: 'Russia',
            price: price,
            description: description,
            image: imgFile.current,
            amount: amount,
            power: power,
          };
          if (imgFile.current != null) {
            switch (activeCategory) {
              case EComponentTypes.videoCard:
                handleCreateProduct<TVideoCard<File>>(
                  '/VideoCard/createVideoCard',
                  requestData as TVideoCard<File>,
                );
            }
          }
        }}
      >
        Создать товар
      </Btn>
    </div>
  );
}

export default AddProduct;
