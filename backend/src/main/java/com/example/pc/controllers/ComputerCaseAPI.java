package com.example.pc.controllers;

import com.example.pc.Models.ComputerCaseModel;
import com.example.pc.Services.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@RestController
@RequestMapping("/api")
public class ComputerCaseAPI {

    private static final Logger logger = LoggerFactory.getLogger(ComputerCaseAPI.class);

    @Autowired
    private ComputerCaseService computerCaseService;

    @PostMapping("/createComponent/ComputerCase")
    public ResponseEntity<Map<String, Object>> createComputerCase(@RequestParam("brand") String brand,
                                                                  @RequestParam("model") String model,
                                                                  @RequestParam("description") String description,
                                                                  @RequestParam("price") Integer price,
                                                                  @RequestParam("country") String country,
                                                                  @RequestParam("material") String material,
                                                                  @RequestParam("width") Integer width,
                                                                  @RequestParam("height") Integer height,
                                                                  @RequestParam("depth") Integer depth,
                                                                  @RequestParam("image") MultipartFile image){

        try{

            ComputerCaseModel computerCaseModel = new ComputerCaseModel();
            computerCaseModel.setBrand(brand);
            computerCaseModel.setModel(model);
            computerCaseModel.setDescription(description);
            computerCaseModel.setPrice(price);
            computerCaseModel.setCountry(country);
            computerCaseModel.setMaterial(material);
            computerCaseModel.setWidth(width);
            computerCaseModel.setHeight(height);
            computerCaseModel.setDepth(depth);
            computerCaseModel.setImage(image.getBytes());

            Map<String, Object> ComputerCaseResponse = new HashMap<>();
            ComputerCaseResponse.put("brand", brand);
            ComputerCaseResponse.put("model", model);
            ComputerCaseResponse.put("description", description);
            ComputerCaseResponse.put("price", price);
            ComputerCaseResponse.put("country", country);
            ComputerCaseResponse.put("material", material);
            ComputerCaseResponse.put("width", width);
            ComputerCaseResponse.put("height", height);
            ComputerCaseResponse.put("depth", depth);
            ComputerCaseResponse.put("image", image.getBytes());

            logger.info("ComputerCase created successfully: {}", ComputerCaseResponse);

            return new ResponseEntity<>(ComputerCaseResponse, HttpStatus.CREATED);

        }catch(IOException e){
            e.printStackTrace();

            logger.error("Failed to create ComputerCase", e);

            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }

}
