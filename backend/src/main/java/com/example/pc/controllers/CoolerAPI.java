package com.example.pc.controllers;

import com.example.pc.Models.CoolerModel;
import com.example.pc.Services.CoolerService;
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
public class CoolerAPI {

    private static final Logger logger = LoggerFactory.getLogger(CoolerAPI.class);

    @Autowired
    private CoolerService coolerService;

    @PostMapping("/createComponent/Cooler")
    public ResponseEntity<Map<String, Object>> createCooler(@RequestParam(name="brand") String brand,
                                                            @RequestParam(name="mode") String model,
                                                            @RequestParam(name="description") String description,
                                                            @RequestParam(name="price") Integer price,
                                                            @RequestParam(name="country") String country,
                                                            @RequestParam(name="speed") Integer speed,
                                                            @RequestParam(name="power") Integer power,
                                                            @RequestParam(name="image")MultipartFile image){

        try{

            if (image.isEmpty()) {
                logger.warn("No image provided");
                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }

            CoolerModel coolerModel = new CoolerModel();
            coolerModel.setBrand(brand);
            coolerModel.setModel(model);
            coolerModel.setDescription(description);
            coolerModel.setPrice(price);
            coolerModel.setCountry(country);
            coolerModel.setSpeed(speed);
            coolerModel.setPower(power);
            coolerModel.setImage(image.getBytes());

            CoolerModel savedCooler = coolerService.addCooler(coolerModel);

            Map<String, Object> CoolerResponse = new HashMap<>();
            CoolerResponse.put("type", "Cooler");
            CoolerResponse.put("id", savedCooler.getId());
            CoolerResponse.put("ComponentData", savedCooler);

            logger.info("Cooler created successfully: {}", savedCooler);

            return new ResponseEntity<>(CoolerResponse, HttpStatus.CREATED);

        }catch(IOException e){
            e.printStackTrace();

            logger.error("Failed to create Cooler:", e);

            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }


    }


}
