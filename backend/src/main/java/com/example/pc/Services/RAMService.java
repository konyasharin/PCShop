package com.example.pc.Services;

import com.example.pc.IServices.IRAMService;
import com.example.pc.Models.RAMModel;
import com.example.pc.dto.RamDto;
import com.example.pc.repositories.RAMRepo;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.List;

@Service
public class RAMService implements IRAMService {

    private final RAMRepo ramRepo;

    private static final Logger logger = LoggerFactory.getLogger(RAMService.class);

    @Autowired
    public RAMService(RAMRepo ramRepo) {
        this.ramRepo = ramRepo;
    }

    @Override
    public List<RAMModel> getRAM() {
        return ramRepo.findAll();
    }

    @Override
    public RAMModel getRAMById(Long id) {
        return ramRepo.findById(id).orElse(null);
    }

    @Override
    public RAMModel updateRAM(RAMModel ramModel) {
        return ramRepo.save(ramModel);
    }

    @Override
    public void deleteRAMById(Long id) {
        ramRepo.deleteById(id);
    }

    @Override
    public RAMModel createRAM(RamDto ramDto){

        try {
            byte[] imageData = ramDto.getImage().getBytes();
            return ramRepo.save(RAMModel.builder()
                    .brand(ramDto.getBrand())
                    .model(ramDto.getModel())
                    .description(ramDto.getDescription())
                    .price(ramDto.getPrice())
                    .country(ramDto.getCountry())
                    .frequency(ramDto.getFrequency())
                    .timings(ramDto.getTimings())
                    .capacity_gb(ramDto.getCapacity_gb())
                    .image(imageData)
                    .build());

        }catch(IOException e){

            logger.error("Failed to convert RAM image to byte array", e);
            throw  new RuntimeException("Failed to convert RAM image to byte array", e);
        }
    }
}
