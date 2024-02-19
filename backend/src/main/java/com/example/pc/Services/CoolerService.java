package com.example.pc.Services;

import com.example.pc.IServices.ICoolerService;
import com.example.pc.Models.CoolerModel;
import com.example.pc.dto.CoolerDto;
import com.example.pc.repositories.CoolerRepo;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.List;

@Service
public class CoolerService implements ICoolerService {

    private final CoolerRepo coolerRepo;

    private static final Logger logger = LoggerFactory.getLogger(CoolerService.class);

    @Autowired
    public CoolerService(CoolerRepo coolerRepo) {
        this.coolerRepo = coolerRepo;
    }

    @Override
    public List<CoolerModel> getAllCoolers() {
        return coolerRepo.findAll();
    }

    @Override
    public CoolerModel getCoolerById(Long id) {
        return coolerRepo.findById(id).orElse(null);
    }

    @Override
    public CoolerModel updateCooler(CoolerModel coolerModel) {
        return coolerRepo.save(coolerModel);
    }

    @Override
    public void deleteCoolerById(Long id) {
        coolerRepo.deleteById(id);
    }

    public CoolerModel createCooler(CoolerDto coolerDto){

        try {
            byte[] imageData = coolerDto.getImage().getBytes();
            return coolerRepo.save(CoolerModel.builder()
                    .brand(coolerDto.getBrand())
                    .model(coolerDto.getModel())
                    .description(coolerDto.getDescription())
                    .price(coolerDto.getPrice())
                    .country(coolerDto.getCountry())
                    .speed(coolerDto.getSpeed())
                    .power(coolerDto.getPower())
                    .image(imageData)
                    .build());
        }catch (IOException e){

            logger.error("Failed to convert Cooler image to byte array", e);
            throw  new RuntimeException("Failed to convert Cooler image to byte array", e);
        }
    }
}
