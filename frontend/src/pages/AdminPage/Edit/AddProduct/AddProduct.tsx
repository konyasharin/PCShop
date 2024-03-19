import CategoriesBlocks from '../CategoriesBlocks/CategoriesBlocks.tsx';
import useRadios from 'hooks/useRadios.ts';
import React, { useEffect, useRef, useState } from 'react';
import EComponentTypes from 'enums/EComponentTypes.ts';
import styles from './AddProduct.module.css';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';
import emptyImg from 'assets/empty-img.png';
import Btn from 'components/btns/Btn/Btn.tsx';
import createProduct from 'api/components/createProduct.ts';
import TVideoCard from 'types/components/TVideoCard.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import useFilters, { TFiltersType, TFilterType } from 'hooks/useFilters.ts';
import TCheckBox from 'types/TCheckBox.ts';

function searchFilter(filters: TFiltersType, name: string): null | TFilterType {
  let result: null | TFilterType = null;
  filters.forEach(filter => {
    if (filter.name === name) {
      result = filter;
    }
  });
  return result;
}

function searchActiveFromFilter(filter: TFilterType): TCheckBox | null {
  let result: null | TCheckBox = null;
  filter.filters.forEach(filterElem => {
    console.log(filter, filterElem);
    if (filterElem.isActive) {
      result = filterElem;
    }
  });
  return result;
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
  const imgFile: React.MutableRefObject<null | File> = useRef(null);
  const allFiltersState = useSelector((state: RootState) => state.filters);
  const { filters, setRadioIsActive, setCheckBoxIsActive, setFiltersHandle } =
    useFilters([], 'radio');
  useEffect(() => {
    setFiltersHandle(allFiltersState[activeCategory]);
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
        filters={filters}
        setCheckBoxIsActive={setCheckBoxIsActive}
        setRadioIsActive={setRadioIsActive}
        imgFileRef={imgFile}
      />
      <Btn
        onClick={() => {
          const brandFilter = searchFilter(filters, 'brand');
          const brand = brandFilter
            ? searchActiveFromFilter(brandFilter)
            : null;
          const modelFilter = searchFilter(filters, 'model');
          const model = modelFilter
            ? searchActiveFromFilter(modelFilter)
            : null;
          const memoryDbFilter = searchFilter(filters, 'memoryDb');
          const memoryDb = memoryDbFilter
            ? searchActiveFromFilter(memoryDbFilter)
            : null;
          const memoryTypeFilter = searchFilter(filters, 'memoryType');
          const memoryType = memoryTypeFilter
            ? searchActiveFromFilter(memoryTypeFilter)
            : null;
          if (imgFile.current != null) {
            createProduct<TVideoCard<File>>('/VideoCard/createVideoCard', {
              brand: brand ? brand.text : '',
              model: model ? model.text : '',
              country: 'Russia',
              price: price,
              description: description,
              image: imgFile.current,
              amount: amount,
              memoryDb: memoryDb ? memoryDb.text : '',
              memoryType: memoryType ? memoryType.text : '',
            });
          }
        }}
      >
        Создать товар
      </Btn>
    </div>
  );
}

export default AddProduct;
