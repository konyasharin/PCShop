package com.example.pc.controllers;

import com.example.pc.Models.SSDModel;
import com.example.pc.Services.SSDService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api")
public class SSDAPI {

    private static final Logger logger = LoggerFactory.getLogger(SSDAPI.class);

    @Autowired
    private SSDService ssdService;

    @PostMapping("/createComponent/SSD")
    public ResponseEntity<Map<String, Object>> createSSD(@RequestParam("brand") String brand,
                                                         @RequestParam("model") String model,
                                                         @RequestParam("description") String description,
                                                         @RequestParam("price") Integer price,
                                                         @RequestParam("country") String country,
                                                         @RequestParam("capacity") Integer capacity,
                                                         @RequestParam("image") MultipartFile image){

        try{

            if (image.isEmpty()) {
                logger.warn("No image provided");
                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }

            SSDModel ssdModel = new SSDModel();
            ssdModel.setBrand(brand);
            ssdModel.setModel(model);
            ssdModel.setDescription(description);
            ssdModel.setPrice(price);
            ssdModel.setCountry(country);
            ssdModel.setCapacity(capacity);
            ssdModel.setImage(image.getBytes());

            SSDModel savedSSD = ssdService.addSSD(ssdModel);

            Map<String, Object> SsdResponse = new HashMap<>();
            SsdResponse.put("type", "SSD");
            SsdResponse.put("id", savedSSD.getId());
            SsdResponse.put("ComponentData", savedSSD);

            logger.info("Ssd created successfully: {}", savedSSD);

            return new ResponseEntity<>(SsdResponse, HttpStatus.CREATED);

        }catch(IOException e){
            e.printStackTrace();

            logger.error("Failed to create Ssd", e);

            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }
}
