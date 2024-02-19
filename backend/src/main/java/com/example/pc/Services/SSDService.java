package com.example.pc.Services;

import com.example.pc.IServices.ISSDService;
import com.example.pc.Models.SSDModel;
import com.example.pc.dto.SsdDto;
import com.example.pc.repositories.SSDRepo;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.List;

@Service
public class SSDService implements ISSDService {

    private final SSDRepo ssdRepo;

    private static final Logger logger = LoggerFactory.getLogger(SSDService.class);

    @Autowired
    public SSDService(SSDRepo ssdRepo) {
        this.ssdRepo = ssdRepo;
    }

    @Override
    public List<SSDModel> getSSD() {
        return ssdRepo.findAll();
    }

    @Override
    public SSDModel getSSDById(Long id) {
        return ssdRepo.findById(id).orElse(null);
    }

    @Override
    public SSDModel updateSSD(SSDModel ssdModel) {
        return ssdRepo.save(ssdModel);
    }

    @Override
    public void deleteSSDById(Long id) {
        ssdRepo.deleteById(id);
    }

    @Override
    public SSDModel createSSD(SsdDto ssdDto){

        try {
            byte[] imageData = ssdDto.getImage().getBytes();
            return ssdRepo.save(SSDModel.builder()
                    .brand(ssdDto.getBrand())
                    .model(ssdDto.getModel())
                    .description(ssdDto.getDescription())
                    .price(ssdDto.getPrice())
                    .country(ssdDto.getCountry())
                    .capacity(ssdDto.getCapacity())
                    .image(imageData)
                    .build());

        }catch(IOException e){

            logger.error("Failed to convert SSD image to byte array", e);
            throw  new RuntimeException("Failed to convert SSD image to byte array", e);
        }
    }

}
