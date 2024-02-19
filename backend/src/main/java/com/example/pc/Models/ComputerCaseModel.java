package com.example.pc.Models;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name="ComputerCase")
@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class ComputerCaseModel {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "brand")
    private String brand;

    @Column(name="model")
    private String model;

    @Column(name = "description")
    private String description;

    @Column(name="price")
    private Integer price;

    @Column(name="country")
    private String country;

    @Column(name="material")
    private String material;

    @Column(name="width")
    private Integer width;

    @Column(name="height")
    private Integer height;

    @Column(name="depth")
    private Integer depth;

    @Lob
    @Column(name = "image")
    private byte[] image;

}
